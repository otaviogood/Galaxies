using System;
using System.Collections.Generic;

namespace Otavio.Math
{
	public struct double2
	{
		public double x;
		public double y;

		public override bool Equals(object obj)
		{
			double2 other = (double2)obj;
			return (other.x == x) && (other.y == y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public double2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
		public double2(double2 f)
		{
			x = f.x;
			y = f.y;
		}
		public double2(int2 i)
		{
			x = (double)i.x;
			y = (double)i.y;
		}
		public double2(double[] f)
		{
			x = f[0];
			y = f[1];
		}

		// math operators
		public static double2 operator +(double2 a, double2 b)
		{
			return new double2(a.x + b.x, a.y + b.y);
		}
		public static double2 operator -(double2 a, double2 b)
		{
			return new double2(a.x - b.x, a.y - b.y);
		}
		public static double2 operator *(double2 a, double2 b)
		{
			return new double2(a.x * b.x, a.y * b.y);
		}
		public static double2 operator *(double2 a, double s)
		{
			return new double2(a.x * s, a.y * s);
		}
		public static double2 operator /(double2 a, double2 b)
		{
			return new double2(a.x / b.x, a.y / b.y);
		}
		public static double2 operator /(double2 a, double s)
		{
			return new double2(a.x / s, a.y / s);
		}

		public static double2 operator -(double2 a)
		{
			return new double2(-a.x, -a.y);
		}

		// comparison operators
		public static bool operator ==(double2 a, double2 b)
		{
			return ((a.x == b.x) && (a.y == b.y));
		}
		public static bool operator !=(double2 a, double2 b)
		{
			return ((a.x != b.x) || (a.y != b.y));
		}

		// math functions
		public double Dot(double2 v)
		{
			return (x * v.x) + (y * v.y);
		}
		public double Length()
		{
			double len = this.Dot(this);
			return (double)System.Math.Sqrt((double)len);
		}
		public double Distance(double2 v)
		{
			double2 delta = v - this;
			double len = delta.Dot(delta);
			return (double)System.Math.Sqrt((double)len);
		}
		public double2 Normalize()
		{
			return this / Length();
		}
		public static double2 Lerp(double2 a, double2 b, double alpha)
		{
			return (a * (1.0f - alpha)) + (b * alpha);
		}
		public double2 Min(double2 v)
		{
			double2 ret = new double2();
			ret.x = x < v.x ? x : v.x;
			ret.y = y < v.y ? y : v.y;
			return ret;
		}
		public double2 Max(double2 v)
		{
			double2 ret = new double2();
			ret.x = x > v.x ? x : v.x;
			ret.y = y > v.y ? y : v.y;
			return ret;
		}
		public double2 Saturate(double2 v)
		{
			return v.Min(new double2(1, 1)).Max(new double2(0, 0));
		}

		public double2 Reflect(double2 normal)
		{
			return this - normal * this.Dot(normal) * 2.0f;
			//v = i - 2 * dot(i, n) * n.
		}
		// This depends on coordinate system.
		const bool Y_IS_UP = false;
		public double2 PerpRight()
		{
#pragma warning disable
			if (Y_IS_UP) return new double2(-y, x);
			else return new double2(y, -x);
#pragma warning restore
		}
		public double2 PerpLeft()
		{
#pragma warning disable
			if (Y_IS_UP) return new double2(y, -x);
			else return new double2(-y, x);
#pragma warning restore
		}
		public double2 PowSign(double v)
		{
			double2 ret = new double2();
			ret.x = System.Math.Sign(x) * (double)System.Math.Pow(System.Math.Abs(x), v);
			ret.y = System.Math.Sign(y) * (double)System.Math.Pow(System.Math.Abs(y), v);
			return ret;
		}
		public double2 Rotate(double rot)
		{
			double2 result = new double2();
			result.x = (double)(System.Math.Cos(rot) * x - System.Math.Sin(rot) * y);
			result.y = (double)(System.Math.Sin(rot) * x + System.Math.Cos(rot) * y);
			return result;
		}

		public override string ToString()
		{
			return x + ", " + y;
		}
	}

	public struct float2
	{
		public float x;
		public float y;

		public override bool Equals(object obj)
		{
			float2 other = ( float2 )obj;
			return (other.x == x) && (other.y == y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode( ) ^ y.GetHashCode( );
		}

		public float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		public float2(float2 f)
		{
			x = f.x;
			y = f.y;
		}
		public float2(int2 i)
		{
			x = (float)i.x;
			y = (float)i.y;
		}
		public float2(float[] f)
		{
			x = f[0];
			y = f[1];
		}

        // math operators
		public static float2 operator+(float2 a, float2 b)
		{
			return new float2(a.x + b.x, a.y + b.y);
		}
		public static float2 operator-(float2 a, float2 b)
		{
			return new float2(a.x - b.x, a.y - b.y);
		}
		public static float2 operator*(float2 a, float2 b)
		{
			return new float2(a.x * b.x, a.y * b.y);
		}
		public static float2 operator*(float2 a, float s)
		{
			return new float2(a.x * s, a.y * s);
		}
		public static float2 operator/(float2 a, float2 b)
		{
			return new float2(a.x / b.x, a.y / b.y);
		}
		public static float2 operator/(float2 a, float s)
		{
			return new float2(a.x / s, a.y / s);
		}

		public static float2 operator-(float2 a)
		{
			return new float2(-a.x, -a.y);
		}

		// comparison operators
		public static bool operator==(float2 a, float2 b)
		{
			return ((a.x == b.x) && (a.y == b.y));
		}
		public static bool operator!=(float2 a, float2 b)
		{
			return ((a.x != b.x) || (a.y != b.y));
		}

        // math functions
        public float Dot(float2 v)
        {
            return (x * v.x) + (y * v.y);
        }
        public float Length()
        {
            float len = this.Dot(this);
            return (float)System.Math.Sqrt((double)len);
        }
		public float Distance(float2 v)
		{
			float2 delta = v - this;
			float len = delta.Dot(delta);
			return (float)System.Math.Sqrt((double)len);
		}
		public float2 Normalize()
        {
			return this / Length();
		}
		public static float2 Lerp(float2 a, float2 b, float alpha)
		{
			return (a * (1.0f - alpha)) + (b * alpha);
		}
		public float2 Min(float2 v)
        {
            float2 ret = new float2();
            ret.x = x < v.x ? x : v.x;
            ret.y = y < v.y ? y : v.y;
            return ret;
        }
        public float2 Max(float2 v)
        {
            float2 ret = new float2();
            ret.x = x > v.x ? x : v.x;
            ret.y = y > v.y ? y : v.y;
            return ret;
        }
		public float2 Saturate(float2 v)
		{
			return v.Min(new float2(1, 1)).Max(new float2(0, 0));
		}

		public float2 Reflect(float2 normal)
		{
			return this - normal * this.Dot(normal) * 2.0f;
			//v = i - 2 * dot(i, n) * n.
		}
		// This depends on coordinate system.
		const bool Y_IS_UP = false;
		public float2 PerpRight()
		{
#pragma warning disable
			if (Y_IS_UP) return new float2(-y, x);
			else return new float2(y, -x);
#pragma warning restore
		}
		public float2 PerpLeft()
		{
#pragma warning disable
			if (Y_IS_UP) return new float2(y, -x);
			else return new float2(-y, x);
#pragma warning restore
		}
		public float2 PowSign(float v)
		{
			float2 ret = new float2();
			ret.x = System.Math.Sign(x) * (float)System.Math.Pow(System.Math.Abs(x), v);
			ret.y = System.Math.Sign(y) * (float)System.Math.Pow(System.Math.Abs(y), v);
			return ret;
		}
		public float2 Rotate(float rot)
		{
			float2 result = new float2();
			result.x = (float)(System.Math.Cos(rot) * x - System.Math.Sin(rot) * y);
			result.y = (float)(System.Math.Sin(rot) * x + System.Math.Cos(rot) * y);
			return result;
		}

		public override string ToString()
		{
			return x + ", " + y;
		}

		// Special functions
		public static bool IntersectCircleWithLine(float2 circlePos, float radius, float2 posA, float2 posB, out float distance)
		{
			// Use dot product along line to find closest point on line
			float dot = (posB - posA).Normalize().Dot(circlePos - posA);
			float2 pointOnLine = (posB - posA).Normalize() * dot + posA;
			// Clamp that point to line end points if outside
			//if ((dot - radius) < 0) pointOnLine = posA;
			if (dot < 0) pointOnLine = posA;
			//if ((dot + radius) > (posB - posA).Length()) pointOnLine = posB;
			if ((dot) > (posB - posA).Length()) pointOnLine = posB;
			// Distance formula from that point to sphere center, compare with radius.
			distance = pointOnLine.Distance(circlePos);
			if (distance > radius) return false;
			return true;
		}
		public static bool IntersectSweptCircleWithLine(float2 circlePos, float circleRadius, float2 circleVel, float2 posA, float2 posB, out float t)
		{
			for (t = 0.0f; t <= 1.0f; t += 0.1f)
			{
				float dist;
				if (IntersectCircleWithLine(circlePos + circleVel * t, circleRadius, posA, posB, out dist)) return true;
			}
			return false;
		}
	}

	public struct int2
	{
		public int x;
		public int y;

		public override bool Equals(object obj)
		{
			int2 other = (int2)obj;
			return (other.x == x) && (other.y == y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public int2(int2 f)
		{
			x = f.x;
			y = f.y;
		}
		public int2(float2 f)
		{
			x = (int)f.x;
			y = (int)f.y;
		}
		public int2(int[] f)
		{
			x = f[0];
			y = f[1];
		}

		// math operators
		public static int2 operator +(int2 a, int2 b)
		{
			return new int2(a.x + b.x, a.y + b.y);
		}
		public static int2 operator -(int2 a, int2 b)
		{
			return new int2(a.x - b.x, a.y - b.y);
		}
		public static int2 operator *(int2 a, int2 b)
		{
			return new int2(a.x * b.x, a.y * b.y);
		}
		public static int2 operator *(int2 a, int s)
		{
			return new int2(a.x * s, a.y * s);
		}
		public static int2 operator /(int2 a, int2 b)
		{
			return new int2(a.x / b.x, a.y / b.y);
		}
		public static int2 operator /(int2 a, int s)
		{
			return new int2(a.x / s, a.y / s);
		}

		public static int2 operator -(int2 a)
		{
			return new int2(-a.x, -a.y);
		}

		// comparison operators
		public static bool operator ==(int2 a, int2 b)
		{
			return ((a.x == b.x) && (a.y == b.y));
		}
		public static bool operator !=(int2 a, int2 b)
		{
			return ((a.x != b.x) || (a.y != b.y));
		}

		// math functions
		public int Dot(int2 v)
		{
			return (x * v.x) + (y * v.y);
		}
		public int Length()
		{
			int len = this.Dot(this);
			return (int)System.Math.Sqrt((double)len);
		}
		public int Distance(int2 v)
		{
			int2 delta = v - this;
			int len = delta.Dot(delta);
			return (int)System.Math.Sqrt((double)len);
		}
		public int2 Normalize()
		{
			return this / Length();
		}
		public static int2 Lerp(int2 a, int2 b, int alpha)
		{
			throw new Exception("Not yet implemented");
		}
		public int2 Min(int2 v)
		{
			int2 ret = new int2();
			ret.x = x < v.x ? x : v.x;
			ret.y = y < v.y ? y : v.y;
			return ret;
		}
		public int2 Max(int2 v)
		{
			int2 ret = new int2();
			ret.x = x > v.x ? x : v.x;
			ret.y = y > v.y ? y : v.y;
			return ret;
		}

		// This depends on coordinate system.
		const bool Y_IS_UP = false;
		public int2 PerpRight()
		{
#pragma warning disable
			if (Y_IS_UP) return new int2(-y, x);
			else return new int2(y, -x);
#pragma warning restore
		}
		public int2 PerpLeft()
		{
#pragma warning disable
			if (Y_IS_UP) return new int2(y, -x);
			else return new int2(-y, x);
#pragma warning restore
		}

		public override string ToString()
		{
			return x + ", " + y;
		}
	}

	// an x,y,z vector
	public struct float3
	{
		public float x;
		public float y;
		public float z;

		public float2 xy
		{
			get { return new float2(x, y); }
		}

		public override bool Equals(object obj)
		{
			float3 other = ( float3 )obj;
			return (other.x == x) && (other.y == y) && (other.z == z);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode( ) ^ y.GetHashCode( ) ^ z.GetHashCode( );
		}

		public float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public float3(float2 v)
		{
			x = v.x;
			y = v.y;
			z = 0.0f;
		}
		public float3(float3 v)
		{
			x = v.x;
			y = v.y;
			z = v.z;
		}
		public float3(float[] f)
		{
			x = f[0];
			y = f[1];
			z = f[2];
		}

		// math operators
		public static float3 operator+(float3 a, float3 b)
		{
			return new float3(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static float3 operator-(float3 a, float3 b)
		{
			return new float3(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static float3 operator*(float3 a, float3 b)
		{
			return new float3(a.x * b.x, a.y * b.y, a.z * b.z);
		}
		public static float3 operator*(float3 a, float s)
		{
			return new float3(a.x * s, a.y * s, a.z * s);
		}
		public static float3 operator/(float3 a, float3 b)
		{
			return new float3(a.x / b.x, a.y / b.y, a.z / b.z);
		}
		public static float3 operator/(float3 a, float s)
		{
			return new float3(a.x / s, a.y / s, a.z / s);
		}

		public static float3 operator-(float3 a)
		{
			return new float3(-a.x, -a.y, -a.z);
		}

		// comparison operators
		public static bool operator>(float3 a, float3 b)
		{
			return ((a.x > b.x) && (a.y > b.y) && (a.z > b.z));
		}
		public static bool operator>=(float3 a, float3 b)
		{
			return ((a.x >= b.x) && (a.y >= b.y) && (a.z >= b.z));
		}
		public static bool operator<(float3 a, float3 b)
		{
			return ((a.x < b.x) && (a.y < b.y) && (a.z < b.z));
		}
		public static bool operator<=(float3 a, float3 b)
		{
			return ((a.x <= b.x) && (a.y <= b.y) && (a.z <= b.z));
		}
		public static bool operator==(float3 a, float3 b)
		{
			return ((a.x == b.x) && (a.y == b.y) && (a.z == b.z));
		}
		public static bool operator!=(float3 a, float3 b)
		{
			return ((a.x != b.x) || (a.y != b.y) || (a.z != b.z));
		}

		// math functions
		public float Dot(float3 v)
		{
			return (x * v.x) + (y * v.y) + (z * v.z);
		}
		public static float dot(float3 a, float3 b)
		{
			return a.Dot(b);
		}
        public float3 Cross(float3 v)
        {
            float3 ret = new float3();
            ret.x = (y * v.z) - (z * v.y);
            ret.y = -((x * v.z) - (v.x * z));
            ret.z = (x * v.y) - (v.x * y);
            return ret;
        }
		public float LengthSquared()
		{
			return this.Dot(this);
		}
		public float Length()
		{
			float len = this.Dot(this);
			return (float)System.Math.Sqrt((double)len);
		}
		public float Len()
		{
			return Length();
		}
		public float Distance(float3 v)
		{
			float3 delta = v - this;
			float len = delta.Dot(delta);
			return (float)System.Math.Sqrt((double)len);
		}
		public float3 Normalize()
		{
			return this / Length();
		}
		public float3 Lerp(float3 v, float alpha)
		{
			return v * alpha + this * (1.0f - alpha);
		}
		public static float3 Lerp(float3 a, float3 b, float alpha)
		{
			return (a * (1.0f - alpha)) + (b * alpha);
		}
		public float3 Pow(float exp)
		{
			return new float3((float)System.Math.Pow(x, exp), (float)System.Math.Pow(y, exp), (float)System.Math.Pow(z, exp));
		}
		public float3 Min(float3 v)
		{
			float3 ret = new float3();
			ret.x = x < v.x ? x : v.x;
			ret.y = y < v.y ? y : v.y;
			ret.z = z < v.z ? z : v.z;
			return ret;
		}
		public float3 Max(float3 v)
		{
			float3 ret = new float3();
			ret.x = x > v.x ? x : v.x;
			ret.y = y > v.y ? y : v.y;
			ret.z = z > v.z ? z : v.z;
			return ret;
		}
		public float3 Clamp(float min, float max)
		{
			return Clamp(new float3(min, min, min), new float3(max, max, max));
		}
		public float3 Clamp(float3 min, float3 max)
		{
			return this.Max(min).Min(max);
		}

		public override string ToString()
		{
			return x + ", " + y + ", " + z;
		}

		public float[] ToArray()
		{
			float[] f = new float[3];
			f[0] = x;
			f[1] = y;
			f[2] = z;
			return f;
		}
		public float this[int index]
		{
			get
			{
				return ToArray()[index];
			}
			set
			{
				float[] f = ToArray();
				f[index] = value;
				this = new float3(f);
			}
		}

		public static bool IntersectSphereAndLine(float3 pos, float radius, float3 posA, float3 posB, out float distance)
		{
			// Use dot product along line to find closest point on line
			float dot = (posB - posA).Normalize().Dot(pos - posA);
			float3 pointOnLine = (posB - posA) * dot;
			// Clamp that point to line end points if outside
			if ((dot - radius) < 0) pointOnLine = posA;
			if ((dot + radius) > (posB-posA).Length()) pointOnLine = posB;
			// Distance formula from that point to sphere center, compare with radius.
			distance = pointOnLine.Distance(pos);
			if (distance > radius) return false;
			return true;
		}

		public static bool IntersectSphereAndTriangle(float3 pos, float radius, float3 posA, float3 posB, float3 posC,
			out float distance, out float3 normal)
		{
			distance = 0;
			normal = (posB - posA).Cross(posC - posA);// CrossProduct(posA, posB, posC);
			if (normal.Length() == 0.0f) throw new Exception("degenerate poly");

			// project sphere center onto triangle, if the projection is inside the triangle, figure out
			// distance and return
			float3 edgeNormal1 = (posB - posA).Cross(normal);
			float3 edgeNormal2 = (posC - posB).Cross(normal);
			float3 edgeNormal3 = (posA - posC).Cross(normal);
			if ((edgeNormal1.Dot(pos - posA) <= 0) &&
				(edgeNormal2.Dot(pos - posB) <= 0) &&
				(edgeNormal3.Dot(pos - posC) <= 0))
			{
				float lengthFromPlane = normal.Normalize().Dot(pos - posA);
				if (lengthFromPlane > radius)
				{
					return false;
				}
				else
				{
					distance = System.Math.Abs(lengthFromPlane);
					return true;
				}
			}

			// Intersect with all edges of the triangle.
			bool intersected;
			intersected = IntersectSphereAndLine(pos, radius, posA, posB, out distance);
			if (intersected) return true;
			intersected = IntersectSphereAndLine(pos, radius, posB, posC, out distance);
			if (intersected) return true;
			intersected = IntersectSphereAndLine(pos, radius, posC, posA, out distance);
			if (intersected) return true;

			return false;
		}

		public static bool IntersectLineAndTriangle(float3 posVec, float3 velVec, float3 posA, float3 posB, float3 posC, out float3 intersection,
										out float t, bool doubleSided)
		{
			float3 normal;
			//normal = (posC - posA).Cross(posB - posA);// CrossProduct(posA, posB, posC);
			normal = (posB - posA).Cross(posC - posA);// CrossProduct(posA, posB, posC);
			if (normal.Length() == 0.0f) throw new Exception("degenerate poly");
			// degenerate cases (line parallel to plane) result in no collision.	
			float3 pointOnPoly;
			pointOnPoly = posB;

			float3 startSide, endSide;
			// get the start position of the line we're projecting on
			// then get the start position of the line relative to the plane
			startSide = pointOnPoly - posVec;

			// next get the end position of the line we're projecting on
			// and make it relative to the start position of the line
			//endSide = velVec - posVec;  // absolute velVec position.
			//if (endSide == new float3(0.0f, 0.0f, 0.0f)) throw new Exception("parallel. not handled yet.");  // parallel movement
			endSide=velVec;  // relative velVec position.

			// Do dot products for intersection
			float startDot, endDot;
			// compute the dot product of the normal of the plane and the start of the line relative to the plane
			startDot = normal.Dot(startSide);
			//	ASSERT(startDot!=0.0);
			// and then compute the dot product of the plane normal, and the length of the vector
			endDot = normal.Dot(endSide);
			//	ASSERT(endDot!=0.0);

			// if both are negative then flip both, to intersect 'double sided'
			if (doubleSided)
			{
				if ((endDot > 0.0f) && (startDot > 0.0f))
				{
					endDot = -endDot;
					startDot = -startDot;
				}
			}

			t = 1.0f;
			intersection = posVec + velVec;
			if ((endDot < 0.0f) && (startDot < 0.0f))
			{	// line is now infinite.
				if (endDot <= startDot)
				{	// Guess there was a line with plane intersection.
					t = startDot / endDot;	// get parametric t variable of where the intersection was.
					//			if (*t<0.0) *t=0.0;
					if ((t < 0.0f) || (t > 1.0f)) throw new Exception("oops");
					intersection = (endSide * t) + posVec;

					float x0, y0, x1, y1, x2, y2, xc, yc; // 2-d polygon coordinates.
					// Project polygon into 2-d to simplify the next step.
					if ((System.Math.Abs(normal.x) >= System.Math.Abs(normal.y)) &&
						 (System.Math.Abs(normal.x) >= System.Math.Abs(normal.z)))
					{
						x0 = posA.z;
						y0 = posA.y;
						x1 = posB.z;
						y1 = posB.y;
						x2 = posC.z;
						y2 = posC.y;
						xc = intersection.z;
						yc = intersection.y;
					}
					else
					{
						if ((System.Math.Abs(normal.y) >= System.Math.Abs(normal.x)) &&
							 (System.Math.Abs(normal.y) >= System.Math.Abs(normal.z)))
						{
							x0 = posA.x;
							y0 = posA.z;
							x1 = posB.x;
							y1 = posB.z;
							x2 = posC.x;
							y2 = posC.z;
							xc = intersection.x;
							yc = intersection.z;
						}
						else
						{ // z is greastest
							x0 = posA.x;
							y0 = posA.y;
							x1 = posB.x;
							y1 = posB.y;
							x2 = posC.x;
							y2 = posC.y;
							xc = intersection.x;
							yc = intersection.y;
						}
					}

					// Do determinants to find out if point of intersection is inside triangle.
					float alpha, beta, u0, v0, u1, v1, u2, v2, quickDot;
					u0 = xc - x1;
					v0 = yc - y1;
					u1 = x2 - x1;
					v1 = y2 - y1;
					u2 = x0 - x1;
					v2 = y0 - y1;
					quickDot = u1 * v2 - u2 * v1;
					if (quickDot != 0.0f)
					{
						alpha = (u0 * v2 - u2 * v0) / quickDot;
						beta = (u1 * v0 - u0 * v1) / quickDot;
						if ((alpha >= 0.0f) && (beta >= 0.0f) && (alpha + beta <= 1.0f))
						{
							// collision happened. Save values or do whatever must be done.
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public struct float4
	{
		public float x;
		public float y;
		public float z;
		public float w;

		public float3 xyz
		{
			get { return new float3(x, y, z); }
			set { x = value.x; y = value.y; z = value.z; }
		}

		public float2 xy
		{
			get { return new float2(x, y); }
		}

		public float2 zw
		{
			get { return new float2(z, w); }
		}

		public override bool Equals(object obj)
		{
			float4 other = (float4)obj;
			return (other.x == x) && (other.y == y) && (other.z == z) && (other.w == w);
		}

		// Negative values ok.
		private static int RotateBitsLeft(int val, int rot)
		{
			uint r1 = (uint)val;
			uint r2 = (uint)val;
			int amount = rot & 31;
			r1 = r1 << amount;
			r2 = r2 >> (32 - amount);
			return (int)(r1 | r2);
		}

		public override int GetHashCode()
		{
			int h = x.GetHashCode() ^ RotateBitsLeft(y.GetHashCode(), 8) ^
				RotateBitsLeft(z.GetHashCode(), 16) ^ RotateBitsLeft(w.GetHashCode(), 24);
			return h;
		}

		public float4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		public float4(float4 v)
		{
			x = v.x;
			y = v.y;
			z = v.z;
			w = v.w;
		}
		public float4(float2 xy, float2 zw)
		{
			x = xy.x;
			y = xy.y;
			z = zw.x;
			w = zw.y;
		}
		public float4(float[] f)
		{
			x = f[0];
			y = f[1];
			z = f[2];
			w = f[3];
		}

		// math operators
        public static float4 operator +(float4 a, float4 b)
        {
            return new float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }
        public static float4 operator -(float4 a, float4 b)
        {
            return new float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        public static float4 operator *(float4 a, float4 b)
        {
            return new float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }
        public static float4 operator *(float4 a, float s)
        {
            return new float4(a.x * s, a.y * s, a.z * s, a.w * s);
        }
        public static float4 operator /(float4 a, float4 b)
        {
            return new float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        }
        public static float4 operator /(float4 a, float s)
        {
            return new float4(a.x / s, a.y / s, a.z / s, a.w / s);
        }

        public static float4 operator -(float4 a)
        {
            return new float4(-a.x, -a.y, -a.z, -a.w);
        }

        // comparison operators
        public static bool operator ==(float4 a, float4 b)
        {
            return ((a.x == b.x) && (a.y == b.y) && (a.z == b.z) && (a.w == b.w));
        }
        public static bool operator !=(float4 a, float4 b)
        {
            return ((a.x != b.x) || (a.y != b.y) || (a.z != b.z) || (a.w != b.w));
        }

		// math functions
		public float Dot(float4 v)
		{
			return (x * v.x) + (y * v.y) + (z * v.z) + (w * v.w);
		}
		public static float dot(float4 a, float4 b)
		{
			return a.Dot(b);
		}
		public float4 Lerp(float4 v, float alpha)
		{
			return v * alpha + this * (1.0f - alpha);
		}
		public static float4 Lerp(float4 a, float4 b, float alpha)
		{
			return (a * (1.0f - alpha)) + (b * alpha);
		}
		public float4 Pow(float exp)
		{
			return new float4((float)System.Math.Pow(x, exp), (float)System.Math.Pow(y, exp),
				(float)System.Math.Pow(z, exp), (float)System.Math.Pow(w, exp));
		}
        public float4 Min(float4 v)
        {
            float4 ret = new float4();
            ret.x = x < v.x ? x : v.x;
            ret.y = y < v.y ? y : v.y;
            ret.z = z < v.z ? z : v.z;
            ret.w = w < v.w ? w : v.w;
            return ret;
        }
        public float4 Max(float4 v)
        {
            float4 ret = new float4();
            ret.x = x > v.x ? x : v.x;
            ret.y = y > v.y ? y : v.y;
            ret.z = z > v.z ? z : v.z;
            ret.w = w > v.w ? w : v.w;
            return ret;
        }
        public float4 Clamp(float min, float max)
        {
            return Clamp(new float4(min, min, min, min), new float4(max, max, max, max));
        }
        public float4 Clamp(float4 min, float4 max)
        {
            return this.Max(min).Min(max);
        }

		public override string ToString()
		{
			return x + ", " + y + ", " + z + ", " + w;
		}

		public float[] ToArray()
		{
			float[] f = new float[4];
			f[0] = x;
			f[1] = y;
			f[2] = z;
			f[3] = w;
			return f;
		}

		public float this[int index]
		{
			get
			{
				if (index == 0) return x;
				if (index == 1) return y;
				if (index == 2) return z;
				return w;
			}
			set
			{
				if (index == 0) x = value;
				if (index == 1) y = value;
				if (index == 2) z = value;
				if (index == 3) w = value;
			}
		}

	}

	// -------------- CODE REVIEWED TO HERE. float2, int2, float3, float4 -----------------------------------------------------

	// a 4x4 matrix
	sealed public class float4x4
	{
		public float[,] m;

		public float4x4()
		{
			m = new float[4,4];
		}
		public float4x4( float[ ] t )
		{
			m = new float[4,4];
			for( int r = 0; r < 4; r++ )
				for( int c = 0; c < 4; c++ )
					m[ c, r ] = t[ r * 4 + c ];
		}
		public float4x4(float3 x, float3 y, float3 z, float3 p)
		{
			m = new float[4,4];
			m[0,0] = x.x;
			m[0,1] = x.y;
			m[0,2] = x.z;
			m[0,3] = 0.0f;
			m[1,0] = y.x;
			m[1,1] = y.y;
			m[1,2] = y.z;
			m[1,3] = 0.0f;
			m[2,0] = z.x;
			m[2,1] = z.y;
			m[2,2] = z.z;
			m[2,3] = 0.0f;
			m[3,0] = p.x;
			m[3,1] = p.y;
			m[3,2] = p.z;
			m[3,3] = 1.0f;
		}
		public float4x4(float4 x, float4 y, float4 z, float4 p)
		{
			m = new float[4,4];
			m[0,0] = x.x;
			m[0,1] = x.y;
			m[0,2] = x.z;
			m[0,3] = x.w;
			m[1,0] = y.x;
			m[1,1] = y.y;
			m[1,2] = y.z;
			m[1,3] = y.w;
			m[2,0] = z.x;
			m[2,1] = z.y;
			m[2,2] = z.z;
			m[2,3] = z.w;
			m[3,0] = p.x;
			m[3,1] = p.y;
			m[3,2] = p.z;
			m[3,3] = p.w;
		}

		public float4x4( Quaternion q )
		{
			m = new float[4,4];

			float ww = q.w * q.w, xx = q.x * q.x, yy = q.y * q.y, zz = q.z * q.z;
			float s = 2.0f / (ww + xx + yy + zz);
			float xy = q.x * q.y, xz = q.x * q.z, yz = q.y * q.z, wx = q.w * q.x, wy = q.w * q.y, wz = q.w * q.z;

			m[0,0] = 1.0f - s * (yy + zz);
			m[1,0] = s * (xy - wz);
			m[2,0] = s * (xz + wy);
			m[3,0] = 0.0f;
			m[0,1] = s * (xy + wz);
			m[1,1] = 1.0f - s * (xx + zz);
			m[2,1] = s * (yz - wx);
			m[3,1] = 0.0f;
			m[0,2] = s * (xz - wy);
			m[1,2] = s * (yz + wx);
			m[2,2] = 1.0f - s * (xx + yy);
			m[3,2] = 0.0f;
			m[0,3] = 0.0f;
			m[1,3] = 0.0f;
			m[2,3] = 0.0f;
			m[3,3] = 1.0f;
		}

		public float[ ] ToArray( )
		{
			float[ ] t = new float[ 16 ];
			for( int r = 0; r < 4; r++ )
				for( int c = 0; c < 4; c++ )
					t[ r * 4 + c ]= m[ c, r ];
			return t;
		}

		// math operators
		public static float4x4 operator*(float4x4 a, float4x4 b)
		{
			float4x4 ret = new float4x4();
			for( int j = 0; j < 4; j++ ) 
			{
				for( int i = 0; i < 4; i++ )
				{
					ret.m[j,i] = (a.m[j,0] * b.m[i,0]) + (a.m[j,1] * b.m[i,1]) + (a.m[j,2] * b.m[i,2]) + (a.m[j,3] * b.m[i,3]);
				}
			}
			return ret;
		}

		public static float3 operator*( float4x4 a, float3 v )
		{
			return new float3( 
				v.x * a.m[0,0] + v.y * a.m[0,1] + v.z * a.m[0,2] + a.m[0,3],
				v.x * a.m[1,0] + v.y * a.m[1,1] + v.z * a.m[1,2] + a.m[1,3],
				v.x * a.m[2,0] + v.y * a.m[2,1] + v.z * a.m[2,2] + a.m[2,3] );
		}

		// math functions
		static public float4x4 Identity()
		{
			float4x4 ret = new float4x4();
			ret.m[0,0] = 1.0f;
			ret.m[0,1] = 0.0f;
			ret.m[0,2] = 0.0f;
			ret.m[0,3] = 0.0f;
			ret.m[1,0] = 0.0f;
			ret.m[1,1] = 1.0f;
			ret.m[1,2] = 0.0f;
			ret.m[1,3] = 0.0f;
			ret.m[2,0] = 0.0f;
			ret.m[2,1] = 0.0f;
			ret.m[2,2] = 1.0f;
			ret.m[2,3] = 0.0f;
			ret.m[3,0] = 0.0f;
			ret.m[3,1] = 0.0f;
			ret.m[3,2] = 0.0f;
			ret.m[3,3] = 1.0f;
			return ret;
		}
		public float4x4 Inverse()
		{
			float4x4 ret = new float4x4();
			return ret;
		}
		public float4x4 OrthogonalInverse()
		{
			float4x4 ret = new float4x4();
			return ret;
		}
		public float4x4 Scale(float3 v)
		{
			return Scale(v.x, v.y, v.z);
		}
		public float4x4 Scale(float x, float y, float z)
		{
			float4x4 s = Identity();
			s.m[0,0] = x;
			s.m[1,1] = y;
			s.m[2,2] = z;
			return this * s;
		}
		public float4x4 Translate(float3 v)
		{
			return Translate(v.x, v.y, v.z);
		}
		public float4x4 Translate(float x, float y, float z)
		{
			float4x4 t = Identity();
			t.m[0,3] = x;
			t.m[1,3] = y;
			t.m[2,3] = z;
			return this * t;
		}
		public float4x4 Rotate(float3 axis, float radians)
		{
			return Rotate(axis.x, axis.y, axis.z, radians);
		}
		public float4x4 Rotate(float x, float y, float z, float radians)
		{
			float4x4 ret = Identity();
			return ret;
		}
		public float4x4 RotateX(float radians)
		{
			float4x4 ret = Identity();
			return ret;
		}
		public float4x4 RotateY(float radians)
		{
			float4x4 ret = Identity();
			return ret;
		}
		public float4x4 RotateZ(float radians)
		{
			float4x4 ret = Identity();
			return ret;
		}
		public float4x4 OrthogonalBasis(float3 zaxis)
		{
			// from PBRT p54
			float3 basisU;
			if( System.Math.Abs(zaxis.x) > System.Math.Abs(zaxis.y) ) 
			{
				float invLen = 1.0f / (float)System.Math.Sqrt((zaxis.x*zaxis.x) + (zaxis.z*zaxis.z));
				basisU = new float3(-zaxis.z * invLen, 0.0f, zaxis.x * invLen);
			} 
			else 
			{
				float invLen = 1.0f / (float)System.Math.Sqrt((zaxis.y*zaxis.y) + (zaxis.z*zaxis.z));
				basisU = new float3(0.0f, zaxis.z * invLen, -zaxis.y * invLen);
			}
			float3 basisV = zaxis.Cross(basisU);

			float4x4 ret = Identity();
			ret.m[0,0] = basisU.x;
			ret.m[0,1] = basisU.y;
			ret.m[0,2] = basisU.z;
			ret.m[1,0] = basisV.x;
			ret.m[1,1] = basisV.y;
			ret.m[1,2] = basisV.z;
			ret.m[2,0] = zaxis.x;
			ret.m[2,1] = zaxis.y;
			ret.m[2,2] = zaxis.z;

			return ret;
		}
		public float4x4 OrthogonalBasis(float3 zaxis, float3 yaxis)
		{
			float4x4 ret = Identity();
			return ret;
		}
	}

	public struct Quaternion
	{
		public Quaternion( float _x, float _y, float _z, float _w )
		{
			x = _x;
			y = _y;
			z = _z;
			w = _w;
		}

		static public Quaternion FromAxisAngle( float3 axis, float theta )
		{
			float sumOfSquares =
				axis.x * axis.x +
				axis.y * axis.y +
				axis.z * axis.z;

			if (sumOfSquares <= 1.0e-5F) 
			{
				return new Quaternion( 0, 0, 0, 1 );
			} 
			else 
			{
				theta *= 0.5f;
				float commonFactor = (float)System.Math.Sin(theta);
				if( sumOfSquares != 1.0 )
					commonFactor /= (float)System.Math.Sqrt(sumOfSquares);
				return new Quaternion( commonFactor * axis.x, 
					commonFactor * axis.y, commonFactor * axis.z, (float)System.Math.Cos(theta) );
			}
		}

		static public Quaternion operator*( Quaternion A, Quaternion B )
		{
			return new Quaternion( 
				A.w*B.x + A.x*B.w + A.y*B.z - A.z*B.y,
				A.w*B.y - A.x*B.z + A.y*B.w + A.z*B.x,
				A.w*B.z + A.x*B.y - A.y*B.x + A.z*B.w,
				A.w*B.w - A.x*B.x - A.y*B.y - A.z*B.z );
		}

		public float Dot( Quaternion B )
		{
			return x * x + y * y + z * z + w * B.w;
		}

		public Quaternion Slerp( Quaternion inquat, float u )
		{
			float theta = this.Dot( inquat );
			if( theta < 0.0f ) 
			{
				inquat.x = -inquat.x;
				inquat.y = -inquat.y;
				inquat.z = -inquat.z;
				inquat.w = -inquat.w;
				theta = -theta;
			} 
			else if( theta >= 1.0f ) 
			{
				return inquat;
			}

			float alp = ( float )System.Math.Acos( theta );

			if( alp < 0.001f ) 
			{
				return inquat;
			}

			float salp = ( float )System.Math.Sin(alp);
			float invsalp = 1.0f / salp;
			float c0 = ( float )System.Math.Sin((1.0f - u) * alp) * invsalp;
			float c1 = ( float )System.Math.Sin(u * alp) * invsalp;

			return new Quaternion( 
			(x * c0) + (inquat.x * c1),
			(y * c0) + (inquat.y * c1),
			(z * c0) + (inquat.z * c1),
			(w * c0) + (inquat.w * c1) );
		}

		public Quaternion Normalize( )
		{
			float magnitude = 1.0f / ( float )System.Math.Sqrt( w * w + x * x + y * y + z * z );
			return new Quaternion( x * magnitude, y * magnitude, z * magnitude, w * magnitude );
		}

		public float x;
		public float y;
		public float z;
		public float w;
	}

	public struct float3x3
	{
		public float m00;
		public float m01;
		public float m02;
		public float m10;
		public float m11;
		public float m12;
		public float m20;
		public float m21;
		public float m22;

		public float3x3(float[] m)
		{
			m00 = m[0];
			m01 = m[1];
			m02 = m[2];
			m10 = m[3];
			m11 = m[4];
			m12 = m[5];
			m20 = m[6];
			m21 = m[7];
			m22 = m[8];
		}
		public float3x3(float3 x, float3 y, float3 z)
		{
			m00 = x.x;
			m01 = x.y;
			m02 = x.z;
			m10 = y.x;
			m11 = y.y;
			m12 = y.z;
			m20 = z.x;
			m21 = z.y;
			m22 = z.z;
		}
		public float3x3(float4 x, float4 y, float4 z)
		{
			m00 = x.x;
			m01 = x.y;
			m02 = x.z;
			m10 = y.x;
			m11 = y.y;
			m12 = y.z;
			m20 = z.x;
			m21 = z.y;
			m22 = z.z;
		}

		public float3 Row0
		{
			get { return new float3(m00, m01, m02); }
			set
			{
				m00 = value.x;
				m01 = value.y;
				m02 = value.z;
			}
		}
		public float3 Row1
		{
			get { return new float3(m10, m11, m12); }
			set
			{
				m10 = value.x;
				m11 = value.y;
				m12 = value.z;
			}
		}
		public float3 Row2
		{
			get { return new float3(m20, m21, m22); }
			set
			{
				m20 = value.x;
				m21 = value.y;
				m22 = value.z;
			}
		}
		public float3 Col0
		{
			get { return new float3(m00, m10, m20); }
			set
			{
				m00 = value.x;
				m10 = value.y;
				m20 = value.z;
			}
		}
		public float3 Col1
		{
			get { return new float3(m01, m11, m21); }
			set
			{
				m01 = value.x;
				m11 = value.y;
				m21 = value.z;
			}
		}
		public float3 Col2
		{
			get { return new float3(m02, m12, m22); }
			set
			{
				m02 = value.x;
				m12 = value.y;
				m22 = value.z;
			}
		}

		public static float3 Mul(float3x3 m, float3 v)
		{
			float3 r;
			r.x = m.Row0.Dot(v);
			r.y = m.Row1.Dot(v);
			r.z = m.Row2.Dot(v);
			return r;
		}
		public static float3 Mul(float3 v, float3x3 m)
		{
			float3 r;
			r.x = m.Col0.Dot(v);
			r.y = m.Col1.Dot(v);
			r.z = m.Col2.Dot(v);
			return r;
		}

		public float Determinant()
		{
			return m00 * (m11 * m22 - m12 * m21) -
					m01 * (m10 * m22 - m12 * m20) +
					m02 * (m10 * m21 - m11 * m20);
		}
		public float3x3 Transpose()
		{
			return new float3x3(Col0, Col1, Col2);
		}
		public float3x3 Inverse()
		{
			float id = 1.0f / Determinant();
			float3x3 r;
			r.m00 = (m11 * m22 - m12 * m21) * id;
			r.m01 = (m02 * m21 - m01 * m22) * id;
			r.m02 = (m01 * m12 - m02 * m11) * id;
			r.m10 = (m12 * m20 - m10 * m22) * id;
			r.m11 = (m00 * m22 - m02 * m20) * id;
			r.m12 = (m02 * m10 - m00 * m12) * id;
			r.m20 = (m10 * m21 - m11 * m20) * id;
			r.m21 = (m01 * m20 - m00 * m21) * id;
			r.m22 = (m00 * m11 - m01 * m10) * id;
			return r;
		}
		public float3 PostMul(float3 b)
		{
			return Mul(this, b);
		}
		//public float2 PostMul(float2 b)
		//{
		//    return new float2(Mul(this, new float3(b.x, b.y, 0.0f)))
		//}
		//public float2 PostMulW1(float2 b)
		//{
		//    return new float2(Mul(this, new float3(b.x, b.y, 1.0f)));
		//}
		public float3 Mul(float3 b)
		{
			return Mul(b, this);
		}
		//public float2 Mul(float2 b)
		//{
		//    return new float2(Mul(new float3(b), this));
		//}
		//public float2 MulW1(float2 b)
		//{
		//    return new float2(Mul(new float3(b.x, b.y, 1.0f), this));
		//}
		public float[] ToArray()
		{
			float[] f = new float[9];
			f[0] = m00;
			f[1] = m01;
			f[2] = m02;
			f[3] = m10;
			f[4] = m11;
			f[5] = m12;
			f[6] = m20;
			f[7] = m21;
			f[8] = m22;
			return f;
		}


		public static float3x3 operator *(float3x3 a, float3x3 b)
		{
			float3x3 r;
			r.m00 = a.Row0.Dot(b.Col0);
			r.m01 = a.Row0.Dot(b.Col1);
			r.m02 = a.Row0.Dot(b.Col2);
			r.m10 = a.Row1.Dot(b.Col0);
			r.m11 = a.Row1.Dot(b.Col1);
			r.m12 = a.Row1.Dot(b.Col2);
			r.m20 = a.Row2.Dot(b.Col0);
			r.m21 = a.Row2.Dot(b.Col1);
			r.m22 = a.Row2.Dot(b.Col2);
			return r;
		}
		public float this[int r, int c]
		{
			get
			{
				return ToArray()[(r * 3) + c];
			}
			set
			{
				float[] f = ToArray();
				f[(r * 3) + c] = value;
				this = new float3x3(f);
			}
		}


		public static float3x3 Identity
		{
			get { return new float3x3(new float3(1.0f, 0.0f, 0.0f), new float3(0.0f, 1.0f, 0.0f), new float3(0.0f, 0.0f, 1.0f)); }
		}
		public static float3x3 Zero
		{
			get { return new float3x3(); }
		}


		public static float3x3 RotateX(float rad)
		{
			float c = (float)System.Math.Cos(rad);
			float s = (float)System.Math.Sin(rad);
			float3x3 r;
			r.m00 = 1.0f;
			r.m01 = 0.0f;
			r.m02 = 0.0f;
			r.m10 = 0.0f;
			r.m11 = c;
			r.m12 = s;
			r.m20 = 0.0f;
			r.m21 = -s;
			r.m22 = c;
			return r;
		}
		public static float3x3 RotateY(float rad)
		{
			float c = (float)System.Math.Cos(rad);
			float s = (float)System.Math.Sin(rad);
			float3x3 r;
			r.m00 = c;
			r.m01 = 0.0f;
			r.m02 = -s;
			r.m10 = 0.0f;
			r.m11 = 1.0f;
			r.m12 = 0.0f;
			r.m20 = s;
			r.m21 = 0.0f;
			r.m22 = c;
			return r;
		}
		public static float3x3 RotateZ(float rad)
		{
			float c = (float)System.Math.Cos(rad);
			float s = (float)System.Math.Sin(rad);
			float3x3 r;
			r.m00 = c;
			r.m01 = s;
			r.m02 = 0.0f;
			r.m10 = -s;
			r.m11 = c;
			r.m12 = 0.0f;
			r.m20 = 0.0f;
			r.m21 = 0.0f;
			r.m22 = 1.0f;
			return r;
		}
		public static float3x3 RotateXYZ(float radX, float radY, float radZ)
		{
			return (RotateX(radX) * RotateY(radY)) * RotateZ(radZ);
		}
		public static float3x3 AxisAngle(float3 v, float rad)
		{
			float s = (float)System.Math.Sin(rad);
			float c = (float)System.Math.Cos(rad);
			float ic = 1.0f - c;
			float3x3 r;
			r.m00 = (v.x * v.x * ic) + c;
			r.m01 = (v.y * v.x * ic) + (v.z * s);
			r.m02 = (v.x * v.z * ic) - (v.y * s);
			r.m10 = (v.x * v.y * ic) - (v.z * s);
			r.m11 = (v.y * v.y * ic) + c;
			r.m12 = (v.y * v.z * ic) + (v.x * s);
			r.m20 = (v.x * v.z * ic) + (v.y * s);
			r.m21 = (v.y * v.z * ic) - (v.x * s);
			r.m22 = (v.z * v.z * ic) + c;
			return r;
		}
		public static float3x3 Scale(float x, float y, float z)
		{
			float3x3 r;
			r.m00 = x;
			r.m01 = 0.0f;
			r.m02 = 0.0f;
			r.m10 = 0.0f;
			r.m11 = y;
			r.m12 = 0.0f;
			r.m20 = 0.0f;
			r.m21 = 0.0f;
			r.m22 = z;
			return r;
		}
		public static float3x3 Basis(float3 z)
		{
			// from PBRT p54
			float3 basisU;
			if (System.Math.Abs(z.x) > System.Math.Abs(z.y))
			{
				float invLen = 1.0f / (float)System.Math.Sqrt((z.x * z.x) + (z.z * z.z));
				basisU = new float3(-z.z * invLen, 0.0f, z.x * invLen);
			}
			else
			{
				float invLen = 1.0f / (float)System.Math.Sqrt((z.y * z.y) + (z.z * z.z));
				basisU = new float3(0.0f, z.z * invLen, -z.y * invLen);
			}
			float3 basisV = z.Cross(basisU);
			return new float3x3(basisU, basisV, z);
		}
		public static float3x3 GenValid3x3FromVector(float3 facing)
		{
			float3 perp1, perp2;
			if (System.Math.Abs(facing.x) > System.Math.Abs(facing.z))
			{
				perp1 = facing.Cross(new float3(0, 0, 1));
			}
			else
			{
				perp1 = facing.Cross(new float3(1, 0, 0));
			}
			perp2 = perp1.Cross(facing);
			return new float3x3(facing.Normalize(), perp1.Normalize(), perp2.Normalize());
		}
		public static float3x3 GenValid3x3FromVectorWithLength(float3 facing)
		{
			float3 perp1, perp2;
			if (System.Math.Abs(facing.x) > System.Math.Abs(facing.z))
			{
				perp1 = facing.Cross(new float3(0, 0, 1));
			}
			else
			{
				perp1 = facing.Cross(new float3(1, 0, 0));
			}
			perp2 = perp1.Cross(facing);
			return new float3x3(facing, perp1.Normalize(), perp2.Normalize());
		}
		//public static float3x3 FromQuaternion(Quaternion q)
		//{
		//    float xs, ys, zs, xx, yy, zz;
		//    float wx, wy, wz, xy, xz, yz;

		//    xs = q.i * 2.0f;
		//    ys = q.j * 2.0f;
		//    zs = q.k * 2.0f;

		//    xx = q.i * xs;
		//    yy = q.j * ys;
		//    zz = q.k * zs;

		//    wx = q.s * xs;
		//    wy = q.s * ys;
		//    wz = q.s * zs;

		//    xy = q.i * ys;
		//    xz = q.i * zs;
		//    yz = q.j * zs;

		//    float3x3 r;
		//    r.m00 = 1.0f - (yy + zz);
		//    r.m01 = xy + wz;
		//    r.m02 = xz - wy;
		//    r.m10 = xy - wz;
		//    r.m11 = 1.0f - (xx + zz);
		//    r.m12 = yz + wx;
		//    r.m20 = xz + wy;
		//    r.m21 = yz - wx;
		//    r.m22 = 1.0f - (xx + yy);
		//    return r;
		//}

	}

	sealed public class BoxI2
	{
		public int2 m_min, m_max;

		public int2 Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		public int2 Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		public BoxI2()
		{
			SetUndefined();
		}

		public BoxI2(int2 min, int2 max)
		{
			m_min = min.Min(max);
			m_max = min.Max(max);
		}

		public BoxI2(Box2 b)
		{
			m_min = new int2(b.Min);
			m_max = new int2(b.Max);
		}

		public void SetUndefined()
		{
			m_min = new int2(int.MaxValue, int.MaxValue);
			m_max = new int2(int.MinValue, int.MinValue);
		}

		public bool IsDefined()
		{ return ((m_min.x <= m_max.x) && (m_min.y <= m_max.y)); }

		public static BoxI2 operator +(BoxI2 b, int2 offset)
		{
			BoxI2 result = b;
			result.m_min += offset;
			result.m_max += offset;
			return result;
		}

		public override bool Equals(object obj)
		{
			BoxI2 other = (BoxI2)obj;
			return other.m_min == m_min && other.m_max == m_max;
		}

		public override int GetHashCode()
		{
			return m_min.GetHashCode() ^ m_max.GetHashCode();
		}

		public static bool operator ==(BoxI2 a, BoxI2 b)
		{
			return ((a.m_min == b.m_min) && (a.m_max == b.m_max));
		}
		public static bool operator !=(BoxI2 a, BoxI2 b)
		{
			return ((a.m_min != b.m_min) || (a.m_max != b.m_max));
		}
		public void MergePoint(int2 point1)
		{
			m_min = m_min.Min(point1);
			m_max = m_max.Max(point1);
		}

		public bool Intersects(BoxI2 box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_max.x < box.m_min.x) return false;
			if (m_min.x > box.m_max.x) return false;
			if (m_max.y < box.m_min.y) return false;
			if (m_min.y > box.m_max.y) return false;
			return true;
		}

		public bool Surrounds(BoxI2 box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_min.x > box.m_min.x) return false;
			if (m_min.y > box.m_min.y) return false;
			if (m_max.x < box.m_max.x) return false;
			if (m_max.y < box.m_max.y) return false;
			return true;
		}

		public bool Contains(int2 point)
		{
			if (!IsDefined()) return false;
			if (m_min.x > point.x) return false;
			if (m_min.y > point.y) return false;
			if (m_max.x < point.x) return false;
			if (m_max.y < point.y) return false;
			return true;
		}

		public void Union(BoxI2 box)
		{
			MergePoint(box.m_min);
			MergePoint(box.m_max);
		}

		public void Intersection(BoxI2 box)
		{
			if (!Intersects(box))
			{
				SetUndefined();
				return;
			}
			m_min = m_min.Max(box.m_min);
			m_max = m_max.Min(box.m_max);
		}

		public int2 GetCentroid()
		{
			return (m_min + m_max) / 2;
		}

		public int2 Size()
		{
			return m_max - m_min;
		}

		public int Area()
		{
			int2 size = Size();
			return size.x * size.y;
		}
	}

	sealed public class Box2
	{
		public float2 m_min, m_max;

		public float2 Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		public float2 Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		public Box2()
		{
			SetUndefined();
		}

		public Box2(float2 min, float2 max)
		{
			m_min = min.Min(max);
			m_max = min.Max(max);
		}

		public void SetUndefined()
		{
			m_min = new float2(float.MaxValue, float.MaxValue);
			m_max = new float2(float.MinValue, float.MinValue);
		}

		public bool IsDefined()
		{ return ((m_min.x <= m_max.x) && (m_min.y <= m_max.y)); }

		public static Box2 operator +(Box2 b, float2 offset)
		{
			Box2 result = b;
			result.m_min += offset;
			result.m_max += offset;
			return result;
		}

		public override bool Equals(object obj)
		{
			Box2 other = (Box2)obj;
			return other.m_min == m_min && other.m_max == m_max;
		}

		public override int GetHashCode()
		{
			return m_min.GetHashCode() ^ m_max.GetHashCode();
		}

		public static bool operator ==(Box2 a, Box2 b)
		{
			return ((a.m_min == b.m_min) && (a.m_max == b.m_max));
		}
		public static bool operator !=(Box2 a, Box2 b)
		{
			return ((a.m_min != b.m_min) || (a.m_max != b.m_max));
		}
		public void MergePoint(float2 point1)
		{
			m_min = m_min.Min(point1);
			m_max = m_max.Max(point1);
		}

		public bool Intersects(Box2 box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_max.x < box.m_min.x) return false;
			if (m_min.x > box.m_max.x) return false;
			if (m_max.y < box.m_min.y) return false;
			if (m_min.y > box.m_max.y) return false;
			return true;
		}

		public bool Surrounds(Box2 box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_min.x > box.m_min.x) return false;
			if (m_min.y > box.m_min.y) return false;
			if (m_max.x < box.m_max.x) return false;
			if (m_max.y < box.m_max.y) return false;
			return true;
		}

		public bool Contains(float2 point)
		{
			if (!IsDefined()) return false;
			if (m_min.x > point.x) return false;
			if (m_min.y > point.y) return false;
			if (m_max.x < point.x) return false;
			if (m_max.y < point.y) return false;
			return true;
		}

		public void Union(Box2 box)
		{
			MergePoint(box.m_min);
			MergePoint(box.m_max);
		}

		public void Intersection(Box2 box)
		{
			if (!Intersects(box))
			{
				SetUndefined();
				return;
			}
			m_min = m_min.Max(box.m_min);
			m_max = m_max.Min(box.m_max);
		}

		public float2 GetCentroid()
		{
			return (m_min + m_max) / 2.0f;
		}

		public float2 Size()
		{
			return m_max - m_min;
		}
	}

	sealed public class Box
	{
		public float3 m_min, m_max;

		public float3 Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		public float3 Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		public Box()
		{
			SetUndefined();
		}

		public Box(float3 min, float3 max)
		{
			m_min = min.Min(max);
			m_max = min.Max(max);
		}

		public void SetUndefined()
		{
			m_min = new float3(float.MaxValue, float.MaxValue, float.MaxValue);
			m_max = new float3(float.MinValue, float.MinValue, float.MinValue);
		}

		public bool IsDefined()
		{ return ((m_min.x <= m_max.x) && (m_min.y <= m_max.y) && (m_min.z <= m_max.z)); }

		public static Box operator +(Box b, float3 offset)
		{
			Box result = b;
			result.m_min += offset;
			result.m_max += offset;
			return result;
		}

		public override bool Equals(object obj)
		{
			Box other = (Box)obj;
			return other.m_min == m_min && other.m_max == m_max;
		}

		public override int GetHashCode()
		{
			return m_min.GetHashCode() ^ m_max.GetHashCode();
		}

		public static bool operator ==(Box a, Box b)
		{
			return ((a.m_min == b.m_min) && (a.m_max == b.m_max));
		}
		public static bool operator !=(Box a, Box b)
		{
			return ((a.m_min != b.m_min) || (a.m_max != b.m_max));
		}
		public void MergePoint(float3 point1)
		{
			m_min = m_min.Min(point1);
			m_max = m_max.Max(point1);
		}

		public bool Intersects(Box box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_max.x < box.m_min.x) return false;
			if (m_min.x > box.m_max.x) return false;
			if (m_max.y < box.m_min.y) return false;
			if (m_min.y > box.m_max.y) return false;
			if (m_max.z < box.m_min.z) return false;
			if (m_min.z > box.m_max.z) return false;
			return true;
		}

		public bool Surrounds(Box box)
		{
			if (!IsDefined()) return false;
			if (!box.IsDefined()) return false;
			if (m_min.x > box.m_min.x) return false;
			if (m_min.y > box.m_min.y) return false;
			if (m_min.z > box.m_min.z) return false;
			if (m_max.x < box.m_max.x) return false;
			if (m_max.y < box.m_max.y) return false;
			if (m_max.z < box.m_max.z) return false;
			return true;
		}

		public bool Contains(float3 point)
		{
			if (!IsDefined()) return false;
			if (m_min.x > point.x) return false;
			if (m_min.y > point.y) return false;
			if (m_min.z > point.z) return false;
			if (m_max.x < point.x) return false;
			if (m_max.y < point.y) return false;
			if (m_max.z < point.z) return false;
			return true;
		}

		public void Union(Box box)
		{
			MergePoint(box.m_min);
			MergePoint(box.m_max);
		}

		public void Intersection(Box box)
		{
			if (!Intersects(box))
			{
				SetUndefined();
				return;
			}
			m_min = m_min.Max(box.m_min);
			m_max = m_max.Min(box.m_max);
		}

		public float3 GetCentroid()
		{
			return (m_min + m_max) / 2.0f;
		}

		public float3 Size()
		{
			return m_max - m_min;
		}
	}


    public class ImprovedPerlinNoise
    {
        static public float noise(float x, float y, float z)
        {
            int X = (int)System.Math.Floor(x) & 255,                  // FIND UNIT CUBE THAT
                Y = (int)System.Math.Floor(y) & 255,                  // CONTAINS POINT.
                Z = (int)System.Math.Floor(z) & 255;
            x -= (float)System.Math.Floor(x);                                // FIND RELATIVE X,Y,Z
			y -= (float)System.Math.Floor(y);                                // OF POINT IN CUBE.
			z -= (float)System.Math.Floor(z);
            float u = fade(x),                                // COMPUTE FADE CURVES
                   v = fade(y),                                // FOR EACH OF X,Y,Z.
                   w = fade(z);
            int A = p[X] + Y, AA = p[A] + Z, AB = p[A + 1] + Z,      // HASH COORDINATES OF
                B = p[X + 1] + Y, BA = p[B] + Z, BB = p[B + 1] + Z;      // THE 8 CUBE CORNERS,

            return lerp(w, lerp(v, lerp(u, grad(p[AA], x, y, z),  // AND ADD
                                           grad(p[BA], x - 1, y, z)), // BLENDED
                                   lerp(u, grad(p[AB], x, y - 1, z),  // RESULTS
                                           grad(p[BB], x - 1, y - 1, z))),// FROM  8
                           lerp(v, lerp(u, grad(p[AA + 1], x, y, z - 1),  // CORNERS
                                           grad(p[BA + 1], x - 1, y, z - 1)), // OF CUBE
                                   lerp(u, grad(p[AB + 1], x, y - 1, z - 1),
                                           grad(p[BB + 1], x - 1, y - 1, z - 1))));
        }

		static public float noise2dWrap(float x, float y, float z, int width, int height)
        {
			x += width * 16;
			y += height * 16;
			x = x % width;
            y = y % height;
            int X = (int)System.Math.Floor(x) & 255,                  // FIND UNIT CUBE THAT
                Y = (int)System.Math.Floor(y) & 255,                  // CONTAINS POINT.
                Z = (int)System.Math.Floor(z) & 255;
            x -= (float)System.Math.Floor(x);                                // FIND RELATIVE X,Y,Z
            y -= (float)System.Math.Floor(y);                                // OF POINT IN CUBE.
            z -= (float)System.Math.Floor(z);
			float u = fade(x),                                // COMPUTE FADE CURVES
                   v = fade(y),                                // FOR EACH OF X,Y,Z.
                   w = fade(z);
            int A = p[X] + Y, AA = p[A] + Z, AB = p[A + 1] + Z,      // HASH COORDINATES OF
                B = p[X + 1] + Y, BA = p[B] + Z, BB = p[B + 1] + Z;      // THE 8 CUBE CORNERS,

            return lerp(w, lerp(v, lerp(u, grad(p[AA], x, y, z),  // AND ADD
                                           grad(p[BA], x - 1, y, z)), // BLENDED
                                   lerp(u, grad(p[AB], x, y - 1, z),  // RESULTS
                                           grad(p[BB], x - 1, y - 1, z))),// FROM  8
                           lerp(v, lerp(u, grad(p[AA + 1], x, y, z - 1),  // CORNERS
                                           grad(p[BA + 1], x - 1, y, z - 1)), // OF CUBE
                                   lerp(u, grad(p[AB + 1], x, y - 1, z - 1),
                                           grad(p[BB + 1], x - 1, y - 1, z - 1))));
        }

		static float fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
		static float lerp(float t, float a, float b)
        {
            return a + t * (b - a);
        }
		static float grad(int hash, float x, float y, float z)
        {
            int h = hash & 15;                      // CONVERT LO 4 BITS OF HASH CODE
			float u = h < 8 ? x : y,                 // INTO 12 GRADIENT DIRECTIONS.
                   v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }
        static int[] p = new int[512];
        static int[] permutation =
        {
            151,160,137,91,90,15,
           131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
           190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
           88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
           77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
           102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
           135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
           5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
           223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
           129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
           251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
           49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
           138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
       };
        static ImprovedPerlinNoise()
        {
            for (int i = 0; i < 256; i++) p[256 + i] = p[i] = permutation[i];
        }
    }
}
